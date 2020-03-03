import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';


import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule} from '@angular/common/http';
import { AppComponent } from './app.component';
import { ValuesComponent } from './values/values.component';
import { NavbarComponent } from './navbar/navbar.component';
import { AuthService } from './_services/auth.service';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { ErrorInterceptorProvider } from './_services/error.interceptor';
import { AlertifyService } from './_services/alertify.service';
import { BsDropdownModule, TabsModule } from 'ngx-bootstrap';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { InfiniteScrollModule } from 'ngx-infinite-scroll'

import { PostsListComponent } from './posts-list/posts-list.component';
import { RouterModule } from '@angular/router';
import { appRoutes } from './app.routes';
import { AuthGuard } from './_guards/auth.guard';
import { UserService } from './_services/user.service';
import { UserComponent } from './user/user.component';
import { JwtModule } from '@auth0/angular-jwt';
import { UserListComponent } from './user-list/user-list.component';
import { UserGuestComponent } from './user-guest/user-guest.component';
import { CreatePostComponent } from './create-post/create-post.component';
import { PostService } from './_services/post.service';

export function tokenGetter(){
   return localStorage.getItem('token');
}


@NgModule({
   declarations: [
      AppComponent,
      ValuesComponent,
      NavbarComponent,
      HomeComponent,
      RegisterComponent,
      PostsListComponent,
      UserComponent,
      UserListComponent,
      UserGuestComponent,
      CreatePostComponent
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      ReactiveFormsModule,
      FormsModule,
      BrowserAnimationsModule,
      InfiniteScrollModule,
      TabsModule.forRoot(),
      BsDropdownModule.forRoot(),
      RouterModule.forRoot(appRoutes),
      JwtModule.forRoot({
         config: {
           tokenGetter: tokenGetter,
           whitelistedDomains: ['localhost:5001']
         }
       })
   
   ],
   providers: [
      AuthService,
      ErrorInterceptorProvider,
      AlertifyService,
      AuthGuard,
      UserService,
      PostService
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
