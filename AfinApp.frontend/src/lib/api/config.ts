import { APIEndpoint } from '@/constants'

export const config = {
    apiUrl: APIEndpoint || 'http://localhost:5455',
    environment: process.env.NODE_ENV || 'development',
    isDevelopment: process.env.NODE_ENV === 'development',
    isProduction: process.env.NODE_ENV === 'production',
} as const

// Debug log to see what URL is being used
console.log('Config loaded with API URL:', config.apiUrl)