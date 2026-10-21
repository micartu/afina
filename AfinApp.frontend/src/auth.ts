import NextAuth from "next-auth";
import Keycloak from "next-auth/providers/keycloak";
import { DrizzleAdapter } from "@auth/drizzle-adapter";
import { db } from "@/lib/db";
import { JWTToken, refreshAccessToken } from "./refreshAccessToken";

export const { handlers, auth, signIn, signOut } = NextAuth({
    adapter: DrizzleAdapter(db),
    providers: [
        Keycloak({
            clientId: process.env.KEYCLOAK_CLIENT_ID as string,
            clientSecret: process.env.KEYCLOAK_CLIENT_SECRET as string,
            issuer: `${process.env.KEYCLOAK_URL}/realms/${process.env.KEYCLOAK_REALM}`,
        }),
    ],
    debug: process.env.NODE_ENV === "development",
    callbacks: {
        authorized({ auth, request: { nextUrl } }) {
            const isLoggedIn = !!auth?.user;
            const isOnLogin = nextUrl.pathname === "/login";
            const isOnHome = nextUrl.pathname === "/";

            if (isLoggedIn) {
                // Redirect from Login or Home to Dashboard
                if (isOnLogin || isOnHome) {
                    return Response.redirect(new URL("/dashboard", nextUrl));
                }
                // allow all other routes
                return true;
            }

            // not logged in
            if (!isLoggedIn) {
                // allow only home or login routes
                if (isOnHome || isOnLogin) {
                    return true;
                }
                // all other routes redirect to home
                return Response.redirect(new URL("/", nextUrl));
            }

            return true;
        },
        jwt: async ({ token, account }) => {
            if (account) {
                token.idToken = account.id_token;
                token.accessToken = account.access_token;
                token.refreshToken = account.refresh_token;

                token.accessTokenExpires = Date.now() + (account.expires_in ?? 0) * 1000;
            }

            const kTimeOffset = 60 * 1000 // 1 minute

            // If token still valid → return it
            if (Date.now() < (token.accessTokenExpires as number ?? 0) - kTimeOffset) {
                return token;
            }

            // Token expired → refresh it
            return await refreshAccessToken(token as JWTToken);
        },
        session({ session, token }) {
            if (token.sub && session.user) {
                session.user.id = token.sub;
            }
            if (token.idToken) {
                session.idToken = token.idToken as string;
            }
            if (token.accessToken) {
                session.accessToken = token.accessToken as string;
            }
            if (token.refreshToken) {
                session.refreshToken = token.refreshToken as string;
            }
            return session;
        },
    },
    pages: {
        signIn: "/login",
        error: "/",
    },
    session: {
        strategy: "jwt",
    },
});

