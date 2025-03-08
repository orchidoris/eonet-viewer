import { EventCategory, EventCategoryId } from '../../clients';
import { Image, ImageProps as MantineImageProps } from '@mantine/core';

import { HTMLAttributes } from 'react';
import classes from './EventCategoryIcon.module.css';
import { cx } from '../../helpers';

interface EventCategoryIconProps extends MantineImageProps {
  category: EventCategory;
}

export function EventCategoryIcon({
  className,
  category: { id, title },
  ...props
}: EventCategoryIconProps & HTMLAttributes<HTMLImageElement>) {
  return <Image src={getIconSrc(id)} alt={title} title={title} className={cx(classes.icon, className)} {...props} />;
}

function getIconSrc(categoryId: EventCategoryId): string {
  switch (categoryId) {
    case EventCategoryId.Drought:
      return '/img/category-icons/drought.png';
    case EventCategoryId.DustHaze:
      return '/img/category-icons/sandstorm.png';
    case EventCategoryId.Earthquakes:
      return '/img/category-icons/earthquake.png';
    case EventCategoryId.Floods:
      return '/img/category-icons/flooded-house.png';
    case EventCategoryId.Landslides:
      return '/img/category-icons/landslide.png';
    case EventCategoryId.Manmade:
      return '/img/category-icons/motivation.png';
    case EventCategoryId.SeaLakeIce:
      return '/img/category-icons/iceberg.png';
    case EventCategoryId.SevereStorms:
      return '/img/category-icons/lightning.png';
    case EventCategoryId.Snow:
      return '/img/category-icons/snow.png';
    case EventCategoryId.TempExtremes:
      return '/img/category-icons/heat.png';
    case EventCategoryId.Volcanoes:
      return '/img/category-icons/volcano.png';
    case EventCategoryId.WaterColor:
      return '/img/category-icons/water.png';
    case EventCategoryId.Wildfires:
      return '/img/category-icons/forest-fire.png';
  }

  return '/img/category-icons/placeholder.png';
}
