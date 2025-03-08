import { Link, To, useLocation } from 'react-router-dom';

import { IconExternalLink } from '@tabler/icons-react';
import { UnstyledButton } from '@mantine/core';
import classes from './NavbarMainLink.module.css';
import { cx } from '../../../../helpers';

interface NavbarMainLinkProps extends React.ComponentPropsWithoutRef<'a'> {
  to?: To;
  children: React.ReactNode;
  icon: React.ReactNode;
  onNavbarClose: () => void;
}

export function NavbarMainLink({ children, icon, onNavbarClose, className, href, to }: NavbarMainLinkProps) {
  const external = !!href;
  const location = useLocation();
  return (
    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    <UnstyledButton<any>
      component={external ? 'a' : Link}
      href={href}
      to={to}
      target={external ? '_blank' : undefined}
      className={cx(classes.link, className)}
      mod={{ active: location.pathname === to }}
      onClick={onNavbarClose}
    >
      <span className={classes.icon}>{icon}</span>
      <span className={classes.label}>{children}</span>
      {external && (
        <span className={classes.external}>
          <IconExternalLink size={14} />
        </span>
      )}
    </UnstyledButton>
  );
}
