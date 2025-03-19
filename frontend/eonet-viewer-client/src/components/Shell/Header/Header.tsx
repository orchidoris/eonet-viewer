import { AppShell, rem } from '@mantine/core';
import { IconCompass, IconFilter, IconLayoutGrid, IconWorldBolt } from '@tabler/icons-react';

import { HeaderMainLink } from './HeaderMainLink';
import classes from './Header.module.css';
import { cx } from '../../../helpers';
import { emitFiltersToggle } from '../../../events';
import nasaLogo from '../../../assets/nasa.svg';

export function Header({ className, ...props }: Readonly<React.HTMLProps<HTMLElement>>) {
  return (
    <AppShell.Header withBorder={false} classNames={{ header: cx(classes.header, className) }} {...props}>
      <div className={classes.left}>
        <img src={nasaLogo} className={classes.logo} alt="EONET Viewer" />
        <h1 className={classes.appName}>EONET Viewer</h1>
        <div className={classes.links}>
          <HeaderMainLink icon={<IconLayoutGrid style={{ width: rem(25), height: rem(25) }} stroke={1.5} />} to="/">
            List
          </HeaderMainLink>
          <HeaderMainLink icon={<IconCompass style={{ width: rem(25), height: rem(25) }} stroke={1.5} />} to="/map">
            Map
          </HeaderMainLink>
          <HeaderMainLink
            icon={<IconWorldBolt style={{ width: rem(25), height: rem(25) }} stroke={1.5} />}
            href="https://eonet.gsfc.nasa.gov/"
          >
            Go to EONET
          </HeaderMainLink>
        </div>
      </div>
      <IconFilter className={classes.iconButton} aria-label="Toggle navbar" stroke={1} onClick={emitFiltersToggle} />
    </AppShell.Header>
  );
}
