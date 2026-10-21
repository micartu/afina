"use client";

import { useState, FormEvent } from "react";
import { useSession } from "next-auth/react";
import { useTodos } from "@/hooks/useTodos";
import { TodoForm } from "./TodoForm";
import { TodoList } from "./TodoList";

export function TodoItems({ accessToken }: { accessToken: string | undefined }) {
	const { data: session, status } = useSession();
	const token = accessToken ?? null;

	// Form state (local to this component)
	const [newTodoName, setNewTodoName] = useState("");

	const { todos, loading, error, submitting, actions } = useTodos({
		accessToken: token,
		enabled: status === "authenticated",
	});

	const handleAddTodo = async (e: FormEvent) => {
		e.preventDefault();

		const name = newTodoName.trim();
		if (!name || submitting) return;

		try {
			await actions.addTodo(name);
			setNewTodoName(""); // Clear input on success
		} catch {
			// Error already handled in hook — optionally keep the value for retry
		}
	};

	// Loading & Auth states
	if (status === "loading") {
		return <div className="animate-pulse p-4">Loading...</div>;
	}

	if (!session) {
		return (
			<div className="p-4 text-red-600" role="alert">
				Not authorized
			</div>
		);
	}

	return (
		<div className="p-4 border rounded-lg max-w-2xl mx-auto">
			<h2 className="text-xl font-semibold mb-4">My Todos</h2>

			<TodoForm
				value={newTodoName}
				onChange={setNewTodoName}
				onSubmit={handleAddTodo}
				disabled={submitting}
			/>

			{error && (
				<div
					className="mb-4 p-3 bg-red-50 text-red-700 rounded-lg text-sm flex items-center justify-between"
					role="alert"
				>
					<span>{error}</span>
					<button
						onClick={actions.clearError}
						className="ml-2 underline hover:text-red-900"
						aria-label="Dismiss error"
					>
						Dismiss
					</button>
				</div>
			)}

			<TodoList
				todos={todos}
				loading={loading}
				onToggle={actions.toggleTodo}
				onDelete={actions.removeTodo}
			/>
		</div>
	);
}