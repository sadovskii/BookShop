import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';

@Component({
  selector: 'app-book',
  templateUrl: './book.component.html',
  styleUrls: ['book.component.scss'],
  standalone: true,
  imports: [CommonModule],
})
export class BookComponent {
}
