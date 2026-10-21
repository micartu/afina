import { ToDoItem } from "@/hooks/useTodos";

type TodoItemProps = {
	todo: ToDoItem;
	onToggle: (todo: ToDoItem) => void;
	onDelete: (id: string) => void;
};

export function TodoItemComponent({ todo, onToggle, onDelete }: TodoItemProps) {
	return (
		<li className="flex items-center gap-3 p-3 border rounded-lg hover:bg-gray-50 transition-colors group">
			<input
				type="checkbox"
				id={`todo-${todo.id}`}
				checked={todo.isCompleted}
				onChange={() => onToggle(todo)}
				className="w-5 h-5 rounded border-gray-300 text-blue-600 focus:ring-blue-500 cursor-pointer"
				aria-label={`Mark "${todo.name}" as ${todo.isCompleted ? "incomplete" : "complete"}`}
			/>
			<label
				htmlFor={`todo-${todo.id}`}
				className={`flex-1 cursor-pointer select-none ${todo.isCompleted ? "line-through text-gray-400" : "text-gray-800"
					}`}
			>
				{todo.name}
			</label>
			<button
				onClick={() => onDelete(todo.id)}
				className="opacity-0 group-hover:opacity-100 px-2 py-1 text-red-600 hover:bg-red-50 rounded transition-all text-sm"
				aria-label={`Delete "${todo.name}"`}
			>
				✕
			</button>
		</li>
	);
}