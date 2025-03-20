import { Outlet } from 'react-router-dom';
import { Shell } from './components';
import { useLoadEventsContextState } from './state';

export function App() {
  useLoadEventsContextState();
  return (
    <Shell>
      <Outlet />
    </Shell>
  );
}
