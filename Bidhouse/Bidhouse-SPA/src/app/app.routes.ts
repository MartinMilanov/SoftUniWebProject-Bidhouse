import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { PostsListComponent } from './posts-list/posts-list.component';
import { AuthGuard } from './_guards/auth.guard';
import { UserComponent } from './user/user.component';
import { UserListComponent } from './user-list/user-list.component';
import { UserGuestComponent } from './user-guest/user-guest.component';

export const appRoutes: Routes = [
   { path:'home',component:HomeComponent},
   { path:'posts',component:PostsListComponent,canActivate:[AuthGuard]},
   { path:'user',component:UserComponent,canActivate:[AuthGuard]},
   { path:'user/:username/:id',component:UserGuestComponent,canActivate:[AuthGuard]},
   { path: 'search/users',component:UserListComponent,canActivate:[AuthGuard]},
   { path:'**',redirectTo:'home',pathMatch:'full'}
];