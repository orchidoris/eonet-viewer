import { AppShell } from '@mantine/core';
import { Outlet } from 'react-router-dom';

export function Root() {
  return (
    <AppShell withBorder={false} header={{ height: '4rem' }}>
      <AppShell.Main>
        <Outlet />
      </AppShell.Main>
    </AppShell>
  );
}
