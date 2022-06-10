import { AfterViewInit, Component, Input, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTable } from '@angular/material/table';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { StundentListDto } from 'src/app/services/api/models';
import { StudentsService } from 'src/app/services/students/students.service';
import { DialogComponent } from '../../dialog/dialog.component';
import { StudentsTableDataSource } from './students-table-datasource';

@Component({
  selector: 'app-students-table',
  templateUrl: './students-table.component.html',
  styleUrls: ['./students-table.component.css']
})
export class StudentsTableComponent implements AfterViewInit, OnInit {
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatTable) table!: MatTable<StundentListDto>;
  dataSource: StudentsTableDataSource;
  isDelete: boolean = false;
  searchstudentForm = this.fb.group({});


  @Input()
  students!: Observable<StundentListDto[]>;;
  /** Columns displayed in the table. Columns IDs can be added, removed, or reordered. */
  displayedColumns = [
    'id',
    'firstName',
    'lastName',
    'age',
    'city',
    'street',
    'zipCode',
    'country',
    'comunity',
    'courseName',
    'category',
    'actions',

  ];

  constructor(private router: Router,
    private studentsService: StudentsService,
    private fb: FormBuilder,
    public dialog: MatDialog,
  ) {
    this.dataSource = new StudentsTableDataSource();
  }




  ngOnInit(): void {
    this.searchstudentForm = this.fb.group({
      firstName: ['',],
      lastName: [''],
      minAge: [, Validators.min(0)],
      maxAge: [, Validators.max(100)],
      city: [''],
      street: [''],
      zipCode: [''],
      country: [''],
      comunity: [''],
      courseName: [''],
      category: [''],
    });

  }

  ngAfterViewInit(): void {
    this.students.subscribe(students => {
      this.loadData(students);
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
        this.deleteStudent(id);
      }
    });
  }


  deleteStudent(id: number) {
    this.studentsService.deleteStudent(id).subscribe(() => {

      this.dataSource.data.filter(student => student.id !== id);
    }
    );
    this.students.subscribe(students => {
      this.loadData(students);
    }
    );
  }


  getDetail(student: any) {
    this.router.navigate(['/main/students/', student.id, student]);
    console.log(student.id,);
  }

  search() {
    this.students = this.studentsService.searchStudents(this.searchstudentForm.value);
    this.dataSource = new StudentsTableDataSource();
    this.students.subscribe(students => {
      this.loadData(students);
    }
    );
  }


  loadData(students: StundentListDto[]) {
    this.dataSource = new StudentsTableDataSource();
    this.dataSource.data = students;
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
    this.table.dataSource = this.dataSource;
  }

}
