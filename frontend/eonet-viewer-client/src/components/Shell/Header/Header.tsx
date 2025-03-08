import { AppShell } from '@mantine/core';
import { IconMenu2 } from '@tabler/icons-react';
import classes from './Header.module.css';
import { cx } from '../../../helpers';
import nasaLogo from '../../../assets/nasa.svg';
import { useCallback } from 'react';

interface HeaderProps {
  onNavbarToggle: () => void;
  onNavbarClose: () => void;
}

export function Header({
  className,
  onNavbarToggle,
  onNavbarClose,
  ...props
}: HeaderProps & Readonly<React.HTMLProps<HTMLElement>>) {
  const onMenuClick = useCallback(
    (e: React.MouseEvent) => {
      onNavbarToggle();
      e.stopPropagation();
    },
    [onNavbarToggle],
  );

  return (
    <AppShell.Header
      withBorder={false}
      classNames={{ header: cx(classes.header, className) }}
      {...props}
      onClick={onNavbarClose}
    >
      <div className={classes.brand}>
        <img src={nasaLogo} className={classes.logo} alt="EONET Viewer" />
        <h1 className={classes.appName}>EONET Viewer</h1>
      </div>
      <IconMenu2 className={classes.burgerButton} onClick={onMenuClick} aria-label="Toggle navbar" stroke={1} />
    </AppShell.Header>
  );
}
