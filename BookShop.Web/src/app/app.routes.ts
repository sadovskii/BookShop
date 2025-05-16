import { Routes } from '@angular/router';
import { BookComponent } from './pages/book/book.component';

export const routes: Routes = [
    {
        path: '**',
        component: BookComponent
    }
];
