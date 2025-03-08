import { AppShell, RemoveScroll, ScrollArea, rem, useDirection } from '@mantine/core';
import { IconCompass, IconLayoutGrid, IconWorldBolt } from '@tabler/icons-react';

import { NavbarMainLink } from './NavbarMainLink/NavbarMainLink';
import classes from './Navbar.module.css';
import { cx } from '../../../helpers';

interface NavbarProps {
  navbarOpened: boolean;
  onNavbarClose: () => void;
}

export function Navbar({ navbarOpened, onNavbarClose: closeNavbar }: NavbarProps) {
  const { dir } = useDirection();

  return (
    <AppShell.Navbar
      id="eonet-viewer-navbar"
      className={cx(classes.navbar, { [RemoveScroll.classNames.zeroRight]: dir === 'ltr' })}
      mod={{ hidden: !navbarOpened }}
    >
      <ScrollArea className={classes.scrollarea} type="never" offsetScrollbars={false}>
        <div className={classes.body}>
          <NavbarMainLink
            icon={<IconLayoutGrid style={{ width: rem(25), height: rem(25) }} stroke={1.5} />}
            to="/"
            onNavbarClose={closeNavbar}
          >
            Events
          </NavbarMainLink>
          <NavbarMainLink
            icon={<IconCompass style={{ width: rem(25), height: rem(25) }} stroke={1.5} />}
            to="/map"
            onNavbarClose={closeNavbar}
          >
            Map
          </NavbarMainLink>
          <NavbarMainLink
            icon={<IconWorldBolt style={{ width: rem(25), height: rem(25) }} stroke={1.5} />}
            href="https://eonet.gsfc.nasa.gov/"
            onNavbarClose={closeNavbar}
          >
            Go to EONET
          </NavbarMainLink>
        </div>
      </ScrollArea>
    </AppShell.Navbar>
  );
}
