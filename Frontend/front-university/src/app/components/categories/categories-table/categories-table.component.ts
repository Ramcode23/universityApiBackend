import { AfterViewInit, Component, Input, ViewChild } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTable } from '@angular/material/table';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { CategoryDto } from 'src/app/services/api/models';
import { CategoriesService } from 'src/app/services/categories/categories.service';
import { DialogComponent } from '../../dialog/dialog.component';
import { CategoriesTableDataSource } from './categories-table-datasource';

@Component({
  selector: 'app-categories-table',
  templateUrl: './categories-table.component.html',
  styleUrls: ['./categories-table.component.css']
})
export class CategoriesTableComponent implements AfterViewInit {
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatTable) table!: MatTable<CategoryDto>;
  dataSource: CategoriesTableDataSource;
  isDelete: boolean = false;
  searchCategoriesForm = this.fb.group({});
  @Input()catiegories$!: Observable<CategoryDto[]>;;
  /** Columns displayed in the table. Columns IDs can be added, removed, or reordered. */
  displayedColumns = ['id', 'name','course', 'actions'];

  constructor(
    private router: Router,
    private categoriesService: CategoriesService,
    private fb: FormBuilder,
    public dialog: MatDialog,


  ) {
    this.dataSource = new CategoriesTableDataSource();
  }

  ngAfterViewInit(): void {

    this.catiegories$.subscribe(categories => {

      this.loadData(categories);
    }
    );
  }



  openDialog(id: number): void {
    const dialogRef = this.dialog.open(DialogComponent, {
      width: '250px',

    });

    dialogRef.afterClosed().subscribe(result => {
      this.isDelete = result;
      console.log(result);
      if (this.isDelete) {
        this.deleteCategory(id);
      }
    });
  }

  getDetail(category: any) {
    this.router.navigate(['/main/categories/', category.id, category]);
  }


  deleteCategory(id: number) {
    this.categoriesService.deleteCategory(id).subscribe(() => {
      this.dataSource.data.filter(category => category.id !== id);
    }
    );

  }



  loadData(categories: CategoryDto[]) {
    this.dataSource = new CategoriesTableDataSource();
    this.dataSource.data = categories;
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
    this.table.dataSource = this.dataSource;
  }



}
