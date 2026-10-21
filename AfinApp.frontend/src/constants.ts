export const APIEndpoint =
    typeof window === "undefined"
        ? process.env.APISERVICE_HTTP
        : "";