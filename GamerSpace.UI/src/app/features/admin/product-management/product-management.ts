import { Component, OnInit } from '@angular/core';
import { PagedResult } from '../../../shared/models/paged-result.model';
import { Product } from '../../../shared/models/product.model';
import { ProductService } from '../../../core/services/product-service';
import { RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-product-management',
  imports: [RouterLink, CommonModule],
  templateUrl: './product-management.html',
  styleUrl: './product-management.scss',
})
export class ProductManagement implements OnInit {
  public productsResult: PagedResult<Product> | null = null;
  public isLoading = true;
  public error: string | null = null;

  constructor(private productService: ProductService) {}

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts(): void {
    this.productService.getProducts(1, 10).subscribe({
      next: (result) => {
        this.productsResult = result;
        this.isLoading = false;
      },
      error: (err) => {
        this.error = 'Falha ao carregar produtos. Tente novamente mais tarde.';
        this.isLoading = false;
        console.error(err);
      },
    });
  }

  deleteProduct(id: number): void {
    if (!confirm('Tem certeza que deseja deletar este produto?')) {
      return;
    }

    this.productService.deleteProduct(id).subscribe({
      next: () => {
        alert('Produto deletado com sucesso!');
        this.loadProducts();
      },
      error: (err) => {
        this.error = err.error.detail || 'Falha ao deletar o produto.';
        console.error(err);
      },
    });
  }
}
