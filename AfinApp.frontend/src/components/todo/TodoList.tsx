import { ToDoItem } from "@/hooks/useTodos";
import { TodoItemComponent } from "./ToDoItemComponent";

type TodoListProps = {
	todos: ToDoItem[];
	loading: boolean;
	onToggle: (todo: ToDoItem) => void;
	onDelete: (id: string) => void;
};

export function TodoList({ todos, loading, onToggle, onDelete }: TodoListProps) {
	if (loading) {
		return (
			<div className="space-y-3" aria-busy="true" aria-live="polite">
				{[1, 2, 3].map((i) => (
					<div
						key={i}
						className="animate-pulse flex items-center gap-3 p-3 border rounded"
					>
						<div className="w-5 h-5 bg-gray-200 rounded" />
						<div className="flex-1 h-4 bg-gray-200 rounded" />
					</div>
				))}
			</div>
		);
	}

	if (todos.length === 0) {
		return (
			<p className="text-gray-500 text-center py-8" role="status">
				No todos yet. Add one above!
			</p>
		);
	}

	return (
		<ul className="space-y-2" role="list">
			{todos.map((todo) => (
				<TodoItemComponent
					key={todo.id}
					todo={todo}
					onToggle={onToggle}
					onDelete={onDelete}
				/>
			))}
		</ul>
	);
}