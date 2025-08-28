//
// <summary>
// TypeScript types for user and authentication data.
// Used throughout Angular app for type safety and API contracts.
// </summary>

// User returned from backend (includes JWT token)
export type User = {
    id: string;
    displayName: string;
    email: string;
    token: string;
    imageUrl?: string;
}

// Credentials for login form
export type LoginCreds = {
    email: string;
    password: string;
}

// Credentials for registration form
export type RegisterCreds = {
    email: string;
    displayName: string;
    password: string;
}