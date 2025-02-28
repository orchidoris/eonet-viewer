import './index.css';

import { MantineProvider, createTheme } from '@mantine/core';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';

import { Notifications } from '@mantine/notifications';
import { Routes as Router } from './Router';
import { RouterProvider } from 'react-router-dom';
import { StrictMode } from 'react';
import { createRoot } from 'react-dom/client';

const theme = createTheme({
  primaryColor: 'teal',
  defaultGradient: { from: 'teal', to: 'cyan', deg: 45 },
});

const queryClient = new QueryClient({
  defaultOptions: {
    queries: {
      refetchOnWindowFocus: false,
      retry: false,
    },
  },
});

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <QueryClientProvider client={queryClient}>
      <MantineProvider theme={theme}>
        <Notifications position="top-right" />
        <RouterProvider router={Router} />
      </MantineProvider>
    </QueryClientProvider>
  </StrictMode>,
);
