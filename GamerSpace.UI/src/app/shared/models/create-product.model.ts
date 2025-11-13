import { CreateProductVariantDto } from './create-product-variant.model';

export interface CreateProductDto {
  name: string;
  description: string | null;
  variants: CreateProductVariantDto[];
}
