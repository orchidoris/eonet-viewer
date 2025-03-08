import { Outlet } from 'react-router-dom';
import { Shell } from './components';

export function App() {
  return (
    <Shell>
      <Outlet />
    </Shell>
  );
}
