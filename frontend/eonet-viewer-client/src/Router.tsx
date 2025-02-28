import { ErrorBoundaryPage, EventsPage, Root } from './pages';

import { createBrowserRouter } from 'react-router-dom';

export const Routes = createBrowserRouter(
  [
    {
      path: '/',
      element: <Root />,
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
