import { Routes } from '@angular/router';
import { ProductList } from './features/products/product-list/product-list';
import { ProductDetail } from './features/products/product-detail/product-detail';
import { CartPage } from './features/cart/cart-page/cart-page';
import { Login } from './features/auth/login/login';
import { adminGuard } from './core/guards/admin-guard';
import { AdminDashboard } from './features/admin/admin-dashboard/admin-dashboard';
import { ProductManagement } from './features/admin/product-management/product-management';
import { ProductForm } from './features/admin/product-form/product-form';
import { Home } from './features/home/home';
import { CategoriesOverview } from './features/categories/overview/overview';
import { AboutUs } from './features/about-us/about-us';

export const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: 'home', component: Home },
  { path: 'categories', component: CategoriesOverview },
  { path: 'about-us', component: AboutUs },
  { path: 'products', component: ProductList },
  { path: 'products/:id', component: ProductDetail },
  { path: 'cart', component: CartPage },
  { path: 'login', component: Login },
  {
    path: 'admin',
    canActivate: [adminGuard],
    children: [
      { path: '', component: AdminDashboard },
      { path: 'products', component: ProductManagement },
      { path: 'products/new', component: ProductForm },
      { path: 'products/edit/:id', component: ProductForm },
    ],
  },
];
