import { Link, To, useLocation } from 'react-router-dom';

import { IconExternalLink } from '@tabler/icons-react';
import { UnstyledButton } from '@mantine/core';
import classes from './HeaderMainLink.module.css';
import { cx } from '../../../../helpers';

interface HeaderMainLinkProps extends React.ComponentPropsWithoutRef<'a'> {
  to?: To;
  children: React.ReactNode;
  icon: React.ReactNode;
}

export function HeaderMainLink({ children, icon, className, href, to }: HeaderMainLinkProps) {
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
