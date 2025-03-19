import { LoadingOverlay } from '@mantine/core';
import classes from './Page.module.css';
import { cx } from '../../helpers';

interface SectionProps {
  isLoading?: boolean;
}

export function Page({
  children,
  className,
  isLoading,
  ...props
}: Readonly<SectionProps> & React.HTMLProps<HTMLDivElement>) {
  return (
    <div className={cx(classes.root, className)} {...props}>
      <LoadingOverlay visible={isLoading} />
      {children}
    </div>
  );
}
