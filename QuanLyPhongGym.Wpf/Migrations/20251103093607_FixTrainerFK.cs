using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyPhongGym.Migrations
{
    /// <inheritdoc />
    public partial class FixTrainerFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_MembershipPlans_MembershipPlanId",
                table: "Members");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutSchedules_Trainers_TrainerId1",
                table: "WorkoutSchedules");

            migrationBuilder.DropIndex(
                name: "IX_WorkoutSchedules_TrainerId1",
                table: "WorkoutSchedules");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "WorkoutSchedules");

            migrationBuilder.DropColumn(
                name: "TrainerId1",
                table: "WorkoutSchedules");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_MembershipPlans_MembershipPlanId",
                table: "Members",
                column: "MembershipPlanId",
                principalTable: "MembershipPlans",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_MembershipPlans_MembershipPlanId",
                table: "Members");

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "WorkoutSchedules",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TrainerId1",
                table: "WorkoutSchedules",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutSchedules_TrainerId1",
                table: "WorkoutSchedules",
                column: "TrainerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_MembershipPlans_MembershipPlanId",
                table: "Members",
                column: "MembershipPlanId",
                principalTable: "MembershipPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutSchedules_Trainers_TrainerId1",
                table: "WorkoutSchedules",
                column: "TrainerId1",
                principalTable: "Trainers",
                principalColumn: "Id");
        }
    }
}
