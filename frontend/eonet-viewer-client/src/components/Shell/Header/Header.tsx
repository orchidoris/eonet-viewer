import { AppShell, Modal, rem } from '@mantine/core';
import { IconCompass, IconFilter, IconLayoutGrid, IconWorldBolt } from '@tabler/icons-react';

import { HeaderMainLink } from './HeaderMainLink';
import classes from './Header.module.css';
import { cx } from '../../../helpers';
import nasaLogo from '../../../assets/nasa.svg';
import { useDisclosure } from '@mantine/hooks';
import { useUIState } from '../../../state';

export function Header({ className, ...props }: Readonly<React.HTMLProps<HTMLElement>>) {
  const { toggleFilters } = useUIState();
  const [mapOpened, { open: openMap, close: closeMap }] = useDisclosure(false);

  return (
    <AppShell.Header withBorder={false} classNames={{ header: cx(classes.header, className) }} {...props}>
      <div className={classes.left}>
        <img src={nasaLogo} className={classes.logo} alt="EONET Viewer" />
        <h1 className={classes.appName}>EONET Viewer</h1>
        <div className={classes.links}>
          <HeaderMainLink icon={<IconLayoutGrid style={{ width: rem(25), height: rem(25) }} stroke={1.5} />} to="/">
            List
          </HeaderMainLink>
          <HeaderMainLink
            icon={<IconCompass style={{ width: rem(25), height: rem(25) }} stroke={1.5} />}
            onClick={openMap}
          >
            Map
          </HeaderMainLink>
          <Modal className={classes.notImplementedModal} opened={mapOpened} onClose={closeMap} withCloseButton={false}>
            This feature is not implemented yet ⌛
          </Modal>
          <HeaderMainLink
            icon={<IconWorldBolt style={{ width: rem(25), height: rem(25) }} stroke={1.5} />}
            href="https://eonet.gsfc.nasa.gov/"
          >
            Go to EONET
          </HeaderMainLink>
        </div>
      </div>
      <IconFilter className={classes.iconButton} aria-label="Toggle navbar" stroke={1} onClick={toggleFilters} />
    </AppShell.Header>
  );
}
