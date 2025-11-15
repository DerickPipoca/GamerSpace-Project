import { CategoryItem } from './category-item.model';

export interface GroupedCategory {
  typeId: number;
  typeName: string;
  items: CategoryItem[];
}
