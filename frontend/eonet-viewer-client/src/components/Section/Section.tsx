import { LoadingOverlay } from '@mantine/core';
import classes from './Section.module.css';
import { cx } from '../../helpers';

interface SectionProps {
  isLoading?: boolean;
}

export function Section({
  children,
  className,
  isLoading,
  ...props
}: Readonly<SectionProps> & React.HTMLProps<HTMLElement>) {
  return (
    <section className={cx(classes.section, className)} {...props}>
      <LoadingOverlay visible={isLoading} />
      {children}
    </section>
  );
}
