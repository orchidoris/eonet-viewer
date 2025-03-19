import { AppShell } from '@mantine/core';
import { Header } from './Header/Header';
import classes from './Shell.module.css';
import { cx } from '../../helpers';

interface ShellProps {
  children: React.ReactNode;
}

export function Shell({ children }: ShellProps) {
  return (
    <AppShell withBorder={false} header={{ height: '4rem' }}>
      <AppShell.Main className={cx(classes.main)}>{children}</AppShell.Main>
      <Header />
    </AppShell>
  );
}
