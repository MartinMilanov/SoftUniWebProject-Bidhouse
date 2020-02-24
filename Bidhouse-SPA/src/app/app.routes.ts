import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { PostsListComponent } from './posts-list/posts-list.component';
import { AuthGuard } from './_guards/auth.guard';

export const appRoutes: Routes = [
   { path:'home',component:HomeComponent},
   { path:'posts',component:PostsListComponent,canActivate:[AuthGuard]},
   { path:'**',redirectTo:'home',pathMatch:'full'}
];