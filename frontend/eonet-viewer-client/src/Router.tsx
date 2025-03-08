import { ErrorBoundaryPage, EventsPage } from './pages';

import { App } from './App';
import { createBrowserRouter } from 'react-router-dom';

export const Routes = createBrowserRouter(
  [
    {
      path: '/',
      element: <App />,
      errorElement: <ErrorBoundaryPage />,
      children: [
        {
          path: '/',
          element: <EventsPage />,
        },
      ],
    },
  ],
  { basename: import.meta.env.BASE_URL },
);
