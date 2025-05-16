import { BookComponent } from './book.component';

describe('BookComponent', () => {
  let component: BookComponent;

  beforeEach(() => {
    component = new BookComponent();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
