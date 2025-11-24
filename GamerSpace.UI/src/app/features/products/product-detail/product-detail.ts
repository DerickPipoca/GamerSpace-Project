import { ProductService } from './../../../core/services/product-service';
import { Component, OnInit, resource } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Product } from '../../../shared/models/product.model';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { CartService } from '../../../core/services/cart-service';
import { ProductVariant } from '../../../shared/models/product-variant.model';
import { forkJoin } from 'rxjs';
import { MatIcon } from '@angular/material/icon';

@Component({
  selector: 'app-product-detail',
  standalone: true,
  imports: [CommonModule, FormsModule, MatIcon],
  templateUrl: './product-detail.html',
  styleUrl: './product-detail.scss',
})
export class ProductDetail implements OnInit {
  public product: Product | null = null;
  public variants: ProductVariant[] = [];
  public selectedVariant: ProductVariant | null = null;
  public quantity: number = 1;

  public isLoading = true;
  public error: string | null = null;

  constructor(
    private productService: ProductService,
    private route: ActivatedRoute,
    private cartService: CartService,
  ) {}

  ngOnInit(): void {
    const idParam = this.route.snapshot.paramMap.get('id');
    if (idParam) {
      const id = Number(idParam);
      this.loadProductDetails(id);
    } else {
      this.error = 'ID do produto não fornecido na URL.';
      this.isLoading = false;
    }
  }

  loadProductDetails(id: number): void {
    this.isLoading = true;
    forkJoin({
      product: this.productService.getProductById(id),
      variants: this.productService.getProductVariants(id),
    }).subscribe({
      next: (result) => {
        this.product = result.product;
        this.variants = result.variants;

        if (this.variants.length > 0) {
          this.selectedVariant = this.variants[0];
        } else {
          this.error = 'Este produto não tem variantes disponíveis.';
        }
        this.isLoading = false;
      },
      error: (err) => {
        this.error = 'Produto não encontrado.';
        this.isLoading = false;
        console.error(err);
      },
    });
  }

  addToCart(): void {
    if (this.product) {
      if (!this.selectedVariant) {
        this.error = 'Por favor, selecione uma variante do produto.';
        return;
      }

      if (this.quantity <= 0) {
        this.error = 'A quantidade deve ser pelo menos 1.';
        return;
      }

      console.log(`Adicionando ${this.quantity} do item ${this.selectedVariant.sku} ao carrinho!`);

      this.cartService.addToCart(this.product, this.selectedVariant, this.quantity);
    }
  }

  changeQuantity(amount: number) {
    if (this.selectedVariant) {
      const newAmount = this.quantity + amount;
      if (newAmount <= this.selectedVariant.stockAmount && newAmount > 0) {
        this.quantity = newAmount;
      }
    }
  }

  public selectVariant(variant: ProductVariant): void {
    this.selectedVariant = variant;
    this.quantity = 1;
  }
}
