namespace LibraryManagementSystem.Api.Dtos.Loans;

public class CreateLoanRequestDto
{
    public Guid BookId { get; set; }

    public Guid BorrowerId { get; set; }

    public DateTime LoanDate { get; set; }
}