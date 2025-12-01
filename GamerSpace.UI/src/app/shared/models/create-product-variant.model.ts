export interface CreateProductVariantDto {
  sku: string;
  price: number;
  description: string | null;
  imageUrl: string | null;
  stockAmount: number;
}
