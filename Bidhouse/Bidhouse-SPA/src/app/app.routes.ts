import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { PostsListComponent } from './posts-list/posts-list.component';
import { AuthGuard } from './_guards/auth.guard';
import { UserComponent } from './user/user.component';
import { UserListComponent } from './user-list/user-list.component';
import { UserGuestComponent } from './user-guest/user-guest.component';
import { CreatePostComponent } from './create-post/create-post.component';
import { PostComponent } from './post/post.component';
import { AdminComponent } from './admin/admin.component';
import { TermsComponent } from './terms/terms.component';
import { PrivacyComponent } from './privacy/privacy.component';

export const appRoutes: Routes = [
   { path:'home',component:HomeComponent},
   { path:'posts',component:PostsListComponent,canActivate:[AuthGuard]},
   { path:'user',component:UserComponent,canActivate:[AuthGuard]},
   { path:'user/:username/:id',component:UserGuestComponent,canActivate:[AuthGuard]},
   { path:'user/create-post',component:CreatePostComponent,canActivate:[AuthGuard]},
   { path:'post/:name/:id',component: PostComponent,canActivate:[AuthGuard]},
   { path: 'search/users',component:UserListComponent,canActivate:[AuthGuard]},
   { path: 'admin',component: AdminComponent, canActivate:[AuthGuard]},
   { path: 'terms',component:TermsComponent},
   { path: 'privacy',component:PrivacyComponent},
   { path:'**',redirectTo:'home',pathMatch:'full'}
];