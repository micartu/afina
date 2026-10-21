import { auth } from "@/auth";
import { redirect } from "next/navigation";
import { SecureLogoutButton } from "@/components/auth/secure-logout-button";
import { TodoItems } from "@/components/todo/TodoItems";

export default async function DashboardPage() {
	const session = await auth();

	if (!session) {
		redirect("/login");
	}

	return (
		<div className="min-h-screen bg-gray-50">
			<nav className="bg-white shadow">
				<div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
					<div className="flex justify-between h-16">
						<div className="flex items-center">
							<h1 className="text-xl font-semibold">Your ToDo Items</h1>
						</div>
						<div className="flex items-center gap-2">
							<SecureLogoutButton />
						</div>
					</div>
				</div>
			</nav>

			<main className="max-w-7xl mx-auto py-6 sm:px-6 lg:px-8">
				<div className="px-4 py-6 sm:px-0">
					<TodoItems accessToken={session.accessToken} />
				</div>
			</main>
		</div>
	);
}