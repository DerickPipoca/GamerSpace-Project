export interface CreateProductVariantDto {
  sku: string;
  price: number;
  description: string | null;
  stockAmount: number;
}
