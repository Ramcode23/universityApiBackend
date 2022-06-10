import { AfterViewInit, Component, Input, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTable } from '@angular/material/table';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { CourseDto } from 'src/app/services/api/models';
import { CoursesService } from 'src/app/services/courses/courses.service';
import { DialogComponent } from '../../dialog/dialog.component';
import { CoursesTableDataSource } from './courses-table-datasource';

@Component({
  selector: 'app-courses-table',
  templateUrl: './courses-table.component.html',
  styleUrls: ['./courses-table.component.css']
})
export class CoursesTableComponent implements AfterViewInit,OnInit {
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatTable) table!: MatTable<CourseDto>;
  dataSource: CoursesTableDataSource;
 @Input() courses$!: Observable<CourseDto[]>;
  isDelete: boolean = false;
  searchCourseForm = this.fb.group({});
  /** Columns displayed in the table. Columns IDs can be added, removed, or reordered. */
  displayedColumns = ['id', 'name','category','students','actions'];

  constructor(
    private coursesService: CoursesService,
    private fb: FormBuilder,
    public dialog: MatDialog,
    private router: Router,

  ) {
    this.dataSource = new CoursesTableDataSource();
  }
  ngOnInit(): void {
    this.searchCourseForm = this.fb.group({
      name: ['',],
      category: [''],
      minStudents: [, Validators.min(0)],
      maxStudents: [, Validators.max(100)],
    });
  }

  ngAfterViewInit(): void {
    this.courses$.subscribe(courses => {
      this.loadData(courses);
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
        this.deleteCourse(id);
      }
    });
  }

  deleteCourse(id: number) {
    this.coursesService.deleteCourse(id).subscribe(() => {
     this.dataSource.data.filter(course => course.id !== id);

    }
    );
    this.courses$.subscribe(courses => {
      this.loadData(courses);
    }
    );
  }


  getDetail(course: any) {
    this.router.navigate(['/main/courses/', course.id, course]);
    console.log(course.id,);
  }

  search() {
     const { name, category, minStudents, maxStudents } = this.searchCourseForm.value;

    this.courses$ = this.coursesService.searchCourse({Name:name, CategoryName:category, RangeStudents: [minStudents||0, maxStudents||0]});
    this.dataSource = new CoursesTableDataSource();
    this.courses$.subscribe(courses => {
      this.loadData(courses);
    },
    error => {
      console.log(error);
    }
    );
  }


  loadData(courses: CourseDto[]) {
    this.dataSource = new CoursesTableDataSource();
    this.dataSource.data = courses;
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
    this.table.dataSource = this.dataSource;
  }





}
