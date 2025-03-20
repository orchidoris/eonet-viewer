interface Env {
  baseUrl: string;
  apiBaseUrl: string;
}

export const env: Env = {
  baseUrl: import.meta.env.BASE_URL,
  apiBaseUrl: import.meta.env.VITE_API_BASE_URL,
};
