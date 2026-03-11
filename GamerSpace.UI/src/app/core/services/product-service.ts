import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PagedResult } from '../../shared/models/paged-result.model';
import { Product } from '../../shared/models/product.model';
import { ProductVariant } from '../../shared/models/product-variant.model';
import { CreateProductDto } from '../../shared/models/create-product.model';
import { UpdateProductDto } from '../../shared/models/update-product.model';
import { UpdateProductVariantDto } from '../../shared/models/update-product-variant.model';
import { CreateProductVariantDto } from '../../shared/models/create-product-variant.model';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  private readonly apiUrl = environment.apiUrl + '/api/products';
  constructor(private http: HttpClient) {}

  public createProduct(dto: CreateProductDto): Observable<Product> {
    return this.http.post<Product>(this.apiUrl, dto);
  }

  public createProductVariant(
    productId: number,
    dto: CreateProductVariantDto,
  ): Observable<ProductVariant> {
    const url = `${this.apiUrl}/${productId}/variants`;
    return this.http.post<ProductVariant>(url, dto);
  }

  public updateProduct(id: number, dto: UpdateProductDto): Observable<void> {
    const url = `${this.apiUrl}/${id}`;
    return this.http.put<void>(url, dto);
  }

  public updateProductVariant(
    productId: number,
    variantId: number,
    dto: UpdateProductVariantDto,
  ): Observable<void> {
    const url = `${this.apiUrl}/${productId}/variants/${variantId}`;
    return this.http.put<void>(url, dto);
  }

  public getProducts(
    pageNumber: number,
    pageSize: number,
    categoryIds: number[] = [],
    searchTerm: string = 'a',
  ): Observable<PagedResult<Product>> {
    let params = new HttpParams()
      .set('pageNumber', pageNumber.toString())
      .set('pageSize', pageSize.toString())
      .set('searchTerm', searchTerm.toString());

    if (categoryIds && categoryIds.length > 0) {
      categoryIds.forEach((id) => {
        params = params.append('CategoryIds', id.toString());
      });
    }

    return this.http.get<PagedResult<Product>>(this.apiUrl, { params });
  }

  public getProductById(id: number) {
    let productVariant = this.http.get<Product>(`${this.apiUrl}/${id}`);
    console.log(productVariant);
    return productVariant;
  }

  public getProductVariants(productId: number): Observable<ProductVariant[]> {
    const url = `${this.apiUrl}/${productId}/variants`;
    return this.http.get<ProductVariant[]>(url);
  }

  public deleteProduct(id: number): Observable<void> {
    const url = `${this.apiUrl}/${id}`;
    return this.http.delete<void>(url);
  }

  public deleteProductVariant(productId: number, variantId: number): Observable<void> {
    const url = `${this.apiUrl}/${productId}/variants/${variantId}`;
    return this.http.delete<void>(url);
  }
}
