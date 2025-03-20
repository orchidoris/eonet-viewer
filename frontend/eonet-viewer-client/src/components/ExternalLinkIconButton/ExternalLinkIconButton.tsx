import { IconExternalLink } from '@tabler/icons-react';
import classes from './ExternalLinkIconButton.module.css';
import { cx } from '../../helpers';

interface ExternalLinkIconButtonProps {
  href?: string;
  title?: string;
  className?: string;
  children?: React.ReactNode;
}

export function ExternalLinkIconButton({ href, title, className, children }: ExternalLinkIconButtonProps) {
  return (
    <a className={classes.root} href={href} target="_blank" rel="noreferrer" title={title}>
      {children}
      <IconExternalLink className={cx(classes.icon, className)} size={14} stroke={1.2} />
    </a>
  );
}
