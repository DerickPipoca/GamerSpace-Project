import { CommonModule } from '@angular/common';
import { ProductService } from './../../../core/services/product-service';
import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { forkJoin, Observable } from 'rxjs';
import { ProductVariant } from '../../../shared/models/product-variant.model';
import { UpdateProductVariantDto } from '../../../shared/models/update-product-variant.model';
import { CreateProductVariantDto } from '../../../shared/models/create-product-variant.model';

@Component({
  selector: 'app-product-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './product-form.html',
  styleUrl: './product-form.scss',
})
export class ProductForm implements OnInit {
  public productForm!: FormGroup;
  public error: string | null = null;
  public isLoading = false;

  private currentProductId: number | null = null;
  public isEditMode = false;

  constructor(
    private fb: FormBuilder,
    private productService: ProductService,
    private route: ActivatedRoute,
    private router: Router,
  ) {}

  ngOnInit(): void {
    const idParam = this.route.snapshot.paramMap.get('id');

    this.productForm = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(3)]],
      description: ['', [Validators.maxLength(1200)]],
      variants: this.fb.array([], Validators.required),
    });

    if (idParam) {
      this.isEditMode = true;
      this.currentProductId = Number(idParam);
      this.isLoading = true;

      forkJoin({
        product: this.productService.getProductById(this.currentProductId),
        variants: this.productService.getProductVariants(this.currentProductId),
      }).subscribe({
        next: (result) => {
          this.productForm.patchValue(result.product);

          this.populateVariantsFormArray(result.variants);
          this.isLoading = false;
        },
        error: (err) => {
          this.error = 'Produto não encontrado.';
          this.isLoading = false;
        },
      });
    } else {
      this.isEditMode = false;
      this.addVariant();
    }
  }

  private populateVariantsFormArray(variants: ProductVariant[]) {
    this.variants.clear();
    variants.forEach((variant) => {
      this.variants.push(
        this.fb.group({
          id: [variant.id],
          sku: [variant.sku, Validators.required],
          price: [variant.price, [Validators.required, Validators.min(0.01)]],
          stockAmount: [variant.stockAmount, [Validators.required, Validators.min(0)]],
          description: [variant.description],
        }),
      );
    });
  }

  get variants(): FormArray {
    return this.productForm.get('variants') as FormArray;
  }

  newVariant(): FormGroup {
    return this.fb.group({
      id: [null],
      sku: ['', Validators.required],
      price: [0, [Validators.required, Validators.min(0.01)]],
      stockAmount: [0, [Validators.required, Validators.min(0)]],
      description: [''],
    });
  }

  addVariant(): void {
    this.variants.push(this.newVariant());
  }

  removeVariant(index: number): void {
    const variantGroup = this.variants.at(index) as FormGroup;
    const variantId = variantGroup.value.id;

    if (!confirm('Tem certeza que deseja remover esta variante?')) {
      return;
    }

    if (this.isEditMode && this.currentProductId && variantId) {
      this.isLoading = true;
      this.productService.deleteProductVariant(this.currentProductId, variantId).subscribe({
        next: () => {
          this.variants.removeAt(index);
          this.isLoading = false;
        },
        error: (err) => {
          this.error = err.error.detail || 'Falha ao deletar a variante.';
          this.isLoading = false;
        },
      });
    } else {
      this.variants.removeAt(index);
    }
  }

  onSubmit(): void {
    if (this.productForm.invalid) {
      this.error = 'Formulário inválido.';
      return;
    }

    this.isLoading = true;
    this.error = null;
    const formData = this.productForm.value;

    if (this.isEditMode && this.currentProductId) {
      const updateTasks: Observable<any>[] = [];

      const updateDto = {
        name: formData.name,
        description: formData.description,
      };

      updateTasks.push(this.productService.updateProduct(this.currentProductId, updateDto));

      this.variants.controls.forEach((control) => {
        const variantForm = control as FormGroup;
        const variantId = variantForm.value.id;

        if (variantId) {
          const variantDto: UpdateProductVariantDto = variantForm.value;
          updateTasks.push(
            this.productService.updateProductVariant(this.currentProductId!, variantId, variantDto),
          );
        } else {
          const variantDto: CreateProductVariantDto = variantForm.value;
          updateTasks.push(
            this.productService.createProductVariant(this.currentProductId!, variantDto),
          );
        }
      });

      forkJoin(updateTasks).subscribe({
        next: () => {
          alert('Produto e variantes atualizado com sucesso!');
          this.isLoading = false;
          this.router.navigate(['/admin/products']);
        },
        error: (err) => {
          this.error = err.error.detail || 'Falha ao atualizar o produto.';
          this.isLoading = false;
        },
      });
    } else {
      const createDto = formData;

      this.productService.createProduct(createDto).subscribe({
        next: () => {
          alert('Produto criado com sucesso!');
          this.router.navigate(['/admin/products']);
        },
        error: (err) => {
          this.error = err.error.detail || 'Falha ao criar o produto.';
          this.isLoading = false;
        },
      });
    }
  }
}
