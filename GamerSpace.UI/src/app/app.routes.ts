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
import { Register } from './features/auth/register/register';
import { NotFound } from './features/not-found/not-found';
import { UnderConstruction } from './features/under-construction/under-construction';
import { ProfileDashboard } from './features/profile/profile-dashboard/profile-dashboard';
import { authGuard } from './core/guards/auth-guard';
import { ProfileLayout } from './features/profile/profile-layout/profile-layout';

export const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: 'home', component: Home },
  { path: 'categories', component: CategoriesOverview },
  { path: 'about-us', component: AboutUs },
  { path: 'products', component: ProductList },
  { path: 'products/:id', component: ProductDetail },
  { path: 'cart', component: CartPage },
  { path: 'login', component: Login },
  { path: 'register', component: Register },
  {
    path: 'profile',
    canActivate: [authGuard],
    component: ProfileLayout,
    children: [{ path: '', component: ProfileDashboard }],
  },
  {
    path: 'admin',
    canActivate: [adminGuard],
    component: ProfileLayout,
    children: [
      { path: '', component: AdminDashboard },
      { path: 'products', component: ProductManagement },
      { path: 'products/new', component: ProductForm },
      { path: 'products/edit/:id', component: ProductForm },
    ],
  },
  { path: 'under-construction', component: UnderConstruction },
  { path: 'not-found', component: NotFound },
  { path: '**', redirectTo: 'not-found', pathMatch: 'full' },
];
