import { config } from './config'
import { getSession } from "next-auth/react";

const API_URL = config.apiUrl

type HttpMethod = 'GET' | 'POST' | 'PUT' | 'DELETE'

async function request<T>(
    method: HttpMethod,
    endpoint: string,
    accessToken: string | null,
    data?: any,
    retry = true
): Promise<T> {
    const url = `${API_URL}${endpoint}`

    console.log(`[API] ${method} ${url}`, data ?? '')

    const headers: Record<string, string> = {
        'Content-Type': 'application/json',
    }

    if (accessToken) {
        headers.Authorization = `Bearer ${accessToken}`
    }

    const response = await fetch(url, {
        method,
        headers,
        body: data ? JSON.stringify(data) : undefined,
    })

    if (response.status === 401 && retry) {
        console.log("[API] 401 → refreshing session...")

        const session = await getSession() // triggers jwt callback

        if (!session?.accessToken) {
            throw new Error("Session expired. Please log in again.")
        }

        return request<T>(method, endpoint, session.accessToken, data, false)
    }

    if (!response.ok) {
        throw new Error(`API error: ${response.status} ${response.statusText}`)
    }

    // Handle empty responses (e.g. DELETE)
    if (response.status === 204) {
        return undefined as T
    }

    return response.json()
}

export const api = {
    get<T>(endpoint: string, accessToken: string | null): Promise<T> {
        return request<T>('GET', endpoint, accessToken)
    },

    post<T>(endpoint: string, accessToken: string | null, data: any): Promise<T> {
        return request<T>('POST', endpoint, accessToken, data)
    },

    put<T>(endpoint: string, accessToken: string | null, data: any): Promise<T> {
        return request<T>('PUT', endpoint, accessToken, data)
    },

    delete(endpoint: string, accessToken: string | null): Promise<void> {
        return request<void>('DELETE', endpoint, accessToken)
    },
}