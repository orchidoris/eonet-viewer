import './index.css';
import '@mantine/core/styles.css';
import '@mantine/notifications/styles.css';
import '@mantine/dates/styles.css';

import { MantineProvider, createTheme } from '@mantine/core';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';

import { ContextsProvider } from './contexts';
import { DatesProvider } from '@mantine/dates';
import { Notifications } from '@mantine/notifications';
import { Routes as Router } from './Router';
import { RouterProvider } from 'react-router-dom';
import { StrictMode } from 'react';
import { createRoot } from 'react-dom/client';

const theme = createTheme({
  colors: {
    blue: [
      '#d0deff',
      '#9ab8ff',
      '#6b99ff',
      '#3e7aff',
      '#1e65f0',
      '#1857d6',
      '#144bc0',
      '#103faa',
      '#0d3494',
      '#09297e',
    ],
  },
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
    <DatesProvider settings={{ locale: 'en', firstDayOfWeek: 1, weekendDays: [0, 6], timezone: 'UTC' }}>
      <QueryClientProvider client={queryClient}>
        <MantineProvider theme={theme}>
          <Notifications position="top-right" />
          <ContextsProvider>
            <RouterProvider router={Router} />
          </ContextsProvider>
        </MantineProvider>
      </QueryClientProvider>
    </DatesProvider>
  </StrictMode>,
);
