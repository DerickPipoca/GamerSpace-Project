import { Component } from '@angular/core';
import { PagedResult } from '../../../shared/models/paged-result.model';
import { Product } from '../../../shared/models/product.model';
import { ProductService } from '../../../core/services/product-service';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './product-list.html',
  styleUrl: './product-list.scss',
})
export class ProductList {
  public productResult: PagedResult<Product> | null = null;
  public isLoading = true;
  public error: string | null = null;

  constructor(private productService: ProductService) {}

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts(): void {
    this.productService.getProducts(1, 10).subscribe({
      next: (result) => {
        this.productResult = result;
        this.isLoading = false;
      },
      error: (err) => {
        this.error = 'Falha ao carregar produtos. Tente novamente mais tarde.';
        this.isLoading = false;
        console.error(err);
      },
    });
  }
}
