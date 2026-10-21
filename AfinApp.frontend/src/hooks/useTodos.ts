"use client";

import { useState, useEffect, useCallback } from "react";
import { api } from "@/lib/api/api-client";

export type ToDoItem = {
	id: string;
	name: string;
	isCompleted: boolean;
	createdAt: string;
	completedAt: string | null;
	userId: string | null;
};

type UseTodosOptions = {
	accessToken: string | null;
	enabled: boolean;
};

const kTodo = "/todo"

export function useTodos({ accessToken, enabled }: UseTodosOptions) {
	const [todos, setTodos] = useState<ToDoItem[]>([]);
	const [loading, setLoading] = useState(true);
	const [error, setError] = useState<string | null>(null);
	const [submitting, setSubmitting] = useState(false);

	const fetchTodos = useCallback(async () => {
		if (!enabled || !accessToken) return;

		try {
			setLoading(true);
			setError(null);
			const data = await api.get<ToDoItem[]>(kTodo, accessToken);
			setTodos(data);
		} catch (err) {
			const message = err instanceof Error ? err.message : "Failed to load todos";
			setError(message);
			console.error("Fetch error:", err);
		} finally {
			setLoading(false);
		}
	}, [accessToken, enabled]);

	useEffect(() => {
		if (enabled) {
			fetchTodos();
		}
	}, [fetchTodos, enabled]);

	const addTodo = async (name: string): Promise<ToDoItem | null> => {
		if (!accessToken || submitting) return null;

		setSubmitting(true);
		setError(null);

		try {
			const payload = { name: name.trim(), isCompleted: false };
			const result = await api.post<ToDoItem>(kTodo, accessToken, payload);
			if (result) {
				setTodos((prev) => [result, ...prev]);
				return result;
			}
			return null;
		} catch (err) {
			const message = err instanceof Error ? err.message : "Failed to add todo";
			setError(message);
			console.error("Create error:", err);
			throw err;
		} finally {
			setSubmitting(false);
		}
	};

	const toggleTodo = async (todo: ToDoItem): Promise<boolean> => {
		if (!accessToken) return false;

		const previousState = [...todos];
		const optimisticUpdate = () => {
			setTodos((prev) =>
				prev.map((t) =>
					t.id === todo.id ? { ...t, isCompleted: !t.isCompleted } : t
				)
			);
		};

		try {
			optimisticUpdate();
			const payload = {
				id: todo.id,
				name: todo.name,
				isCompleted: !todo.isCompleted,
			};
			await api.put<ToDoItem>(`${kTodo}/${todo.id}`, accessToken, payload);
			return true;
		} catch (err) {
			setTodos(previousState); // Rollback on error
			const message = err instanceof Error ? err.message : "Failed to update todo";
			setError(message);
			console.error("Update error:", err);
			return false;
		}
	};

	const removeTodo = async (id: string): Promise<boolean> => {
		if (!accessToken) return false;

		try {
			await api.delete(`${kTodo}/${id}`, accessToken);
			setTodos((prev) => prev.filter((t) => t.id !== id));
			return true;
		} catch (err) {
			const message = err instanceof Error ? err.message : "Failed to delete todo";
			setError(message);
			console.error("Delete error:", err);
			return false;
		}
	};

	const clearError = () => setError(null);

	return {
		todos,
		loading,
		error,
		submitting,
		actions: {
			addTodo,
			toggleTodo,
			removeTodo,
			refetch: fetchTodos,
			clearError,
		},
	};
}