export interface PagedResult<T> {
  pageNumber: number;
  pageSize: number;
  totalPages: number;
  totalRecords: number;
  items: T[];
}
