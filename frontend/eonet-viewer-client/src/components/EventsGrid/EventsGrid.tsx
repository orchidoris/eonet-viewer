import { Card, Code, Flex, SimpleGrid, SimpleGridProps, Text } from '@mantine/core';

import { Event } from '../../clients';
import { EventCategoryIcon } from '../EventCategoryIcon';
import { ExternalLinkIconButton } from '../ExternalLinkIconButton';
import classes from './EventsGrid.module.css';
import { cx } from '../../helpers';
import { useEventsContext } from '../../contexts';

interface EventsGridProps extends SimpleGridProps {
  events?: Event[];
}

export function EventsGrid({ events, className, ...props }: EventsGridProps) {
  const { sources } = useEventsContext();
  return (
    <SimpleGrid
      className={cx(classes.root, className)}
      cols={{ base: 1, xs: 2, md: 3, lg: 4, xl: 5 }}
      verticalSpacing="lg"
      {...props}
    >
      {events?.map((event) => (
        <Card key={event.id} className={classes.card} shadow="sm" padding="lg" radius="md" withBorder>
          <Flex className={classes.categories} gap="xs">
            {event.categories.map((category) => (
              <Code key={category.id}>{category.title}</Code>
            ))}
          </Flex>
          <ExternalLinkIconButton
            className={classes.sourceExternalLinkIconButton}
            href={event.sources[0].externalSourceEventUrl}
            title={sources[event.sources[0].id]?.title}
          />
          <Flex className={classes.header} gap="sm">
            <EventCategoryIcon className={classes.icon} categoryId={event.categories[0].id} />
            <Text fw={500} lineClamp={2}>
              {event.title}
            </Text>
          </Flex>
          {event.description ? (
            <Text size="sm" c="dimmed" mt="sm" lineClamp={3}>
              {event.description}
            </Text>
          ) : undefined}
        </Card>
      ))}
    </SimpleGrid>
  );
}
