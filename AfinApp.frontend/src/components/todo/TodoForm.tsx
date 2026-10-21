import { FormEvent } from "react";

type TodoFormProps = {
	value: string;
	onChange: (value: string) => void;
	onSubmit: (e: FormEvent) => void;
	disabled: boolean;
};

export function TodoForm({ value, onChange, onSubmit, disabled }: TodoFormProps) {
	return (
		<form onSubmit={onSubmit} className="flex gap-2 mb-6" role="search">
			<input
				type="text"
				value={value}
				onChange={(e) => onChange(e.target.value)}
				placeholder="Add a new todo..."
				className="flex-1 px-3 py-2 border rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-500"
				disabled={disabled}
				aria-label="New todo name"
			/>
			<button
				type="submit"
				disabled={disabled || !value.trim()}
				className="px-4 py-2 bg-blue-600 text-white rounded-lg hover:bg-blue-700 disabled:opacity-50 disabled:cursor-not-allowed transition-colors"
				aria-label="Add todo"
			>
				{disabled ? "…" : "+"}
			</button>
		</form>
	);
}