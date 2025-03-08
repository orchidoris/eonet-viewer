import { AppShell, Overlay } from '@mantine/core';

import { Header } from './Header/Header';
import { Navbar } from './Navbar/Navbar';
import classes from './Shell.module.css';
import { useUncontrolled } from '@mantine/hooks';

interface ShellProps {
  children: React.ReactNode;
  navbarOpened?: boolean;
  onNavbarOpenedChange?: (opened: boolean) => void;
}

export function Shell({ children, navbarOpened: initialNavbarOpened, onNavbarOpenedChange }: ShellProps) {
  const [navbarOpened, setNavbarOpened] = useUncontrolled({
    value: initialNavbarOpened,
    defaultValue: false,
    finalValue: false,
    onChange: onNavbarOpenedChange,
  });

  const toggleNavbar = () => setNavbarOpened(!navbarOpened);
  const closeNavbar = () => setNavbarOpened(false);

  return (
    <>
      <AppShell withBorder={false} header={{ height: '4rem' }}>
        <AppShell.Main className={classes.main}>{children}</AppShell.Main>
        <Header onNavbarToggle={toggleNavbar} onNavbarClose={closeNavbar} />
        {navbarOpened && <Overlay opacity={0} onClick={closeNavbar} zIndex={99} />}
        <Navbar navbarOpened={navbarOpened} onNavbarClose={closeNavbar} />
      </AppShell>
    </>
  );
}
