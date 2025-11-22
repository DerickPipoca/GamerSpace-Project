import { Component, Input, input } from '@angular/core';
import { PagedResult } from '../../../shared/models/paged-result.model';
import { Product } from '../../../shared/models/product.model';
import { ProductService } from '../../../core/services/product-service';
import { CommonModule } from '@angular/common';
import { GroupedCategory } from '../../../shared/models/category-group.model';
import { CategoryService } from '../../../core/services/category-service';
import { ProductCard } from '../../../layout/product-card/product-card';
import { ActivatedRoute, ParamMap } from '@angular/router';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [CommonModule, ProductCard],
  templateUrl: './product-list.html',
  styleUrl: './product-list.scss',
})
export class ProductList {
  public categoryIds: number[] = [];
  public searchTerm: string = '';

  public productResult: PagedResult<Product> | null = null;
  public productIsLoading: boolean = true;
  public productError: string | null = null;

  public categoryGroupResult: GroupedCategory[] = [];
  public categoryIsLoading: boolean = true;
  public categoryError: string | null = null;

  constructor(
    private productService: ProductService,
    private categoryService: CategoryService,
    private route: ActivatedRoute,
  ) {}

  ngOnInit(): void {
    this.route.queryParamMap.subscribe((params: ParamMap) => {
      this.searchTerm = '';
      const textSearched = params.get('searchTerm');
      if (textSearched) {
        this.searchTerm = textSearched ? textSearched : '';
      }
      this.loadProducts();
      this.loadCategories();
    });
  }

  loadCategories(): void {
    this.categoryService.getCategories().subscribe({
      next: (result) => {
        const groupMap = new Map<number, GroupedCategory>();

        for (const category of result) {
          let group = groupMap.get(category.typeId);

          if (!group) {
            group = {
              typeId: category.typeId,
              typeName: category.typeName,
              items: [],
            };
            groupMap.set(category.typeId, group);
          }

          group.items.push({
            id: category.id,
            name: category.name,
          });
        }

        this.categoryGroupResult = Array.from(groupMap.values());
        this.categoryIsLoading = false;
      },
      error: (err) => {
        this.categoryError = 'Falha ao carregar categorias. Tente novamente mais tarde.';
        this.categoryIsLoading = false;
        console.error(err);
      },
    });
  }

  loadProducts(): void {
    this.productService.getProducts(1, 10, this.categoryIds, this.searchTerm).subscribe({
      next: (result) => {
        this.productResult = result;
        this.productIsLoading = false;
      },
      error: (err) => {
        this.productError = 'Falha ao carregar produtos. Tente novamente mais tarde.';
        this.productIsLoading = false;
        console.error(err);
      },
    });
  }
  onCategoryChange(event: any, categoryId: number): void {
    const isChecked = event.target.checked;

    if (isChecked) {
      this.categoryIds.push(categoryId);
    } else {
      this.categoryIds = this.categoryIds.filter((id) => id !== categoryId);
    }
  }
  searchWithCategory() {
    this.loadProducts();
  }
}
