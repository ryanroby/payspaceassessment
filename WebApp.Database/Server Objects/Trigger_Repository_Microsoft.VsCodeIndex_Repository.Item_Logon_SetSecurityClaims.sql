create trigger [Trigger_Repository_Microsoft.VsCodeIndex_Repository.Item_Logon_SetSecurityClaims]
    on all server
    for logon
    as
    begin
      begin try
        if has_perms_by_name(N'[Microsoft.VsCodeIndex].[Repository.Item].[InitializeSession]', N'OBJECT', N'EXECUTE') = 1
        begin
          execute [Microsoft.VsCodeIndex].[Repository.Item].[InitializeSession];
        end
        else if db_id(N'Microsoft.VsCodeIndex') is null
        begin
          declare @warningNoDatabaseMessage nvarchar(165) = N'Error : The database [Microsoft.VsCodeIndex] does not exist.';

          if exists (select *                            -- Caller has Alter Trace permissions.
                     from sys.server_permissions as SP
                     where SP.class = 100 and
                           SP.type in ('ALTR', 'CL') and
                           SP.State in ('G', 'W'))
          begin
            raiserror(@warningNoDatabaseMessage, 0, 0) with log, nowait;
          end
          else
          begin
            raiserror(@warningNoDatabaseMessage, 0, 0) with nowait;
          end;

          if exists (select * from sys.server_triggers where name = N'Trigger_Repository_Microsoft.VsCodeIndex_Drop')
          begin
            drop trigger [Trigger_Repository_Microsoft.VsCodeIndex_Drop] on all server;
          end;

          if exists (select * from sys.server_triggers where name = N'Trigger_Repository_Microsoft.VsCodeIndex_Repository.Item_Logon_SetSecurityClaims')
          begin
            drop trigger [Trigger_Repository_Microsoft.VsCodeIndex_Repository.Item_Logon_SetSecurityClaims] on all server;
          end;
        end;
      end try
      begin catch
        if @@trancount > 0
        begin
          rollback transaction;
        end;

        declare @errorNumber int = error_number();
        declare @errorSeverity int = error_severity();
        declare @errorState int = error_state();
        declare @errorProcedure nvarchar(126) = error_procedure(); -- error_procedure does not return a sysname.
        declare @errorLine int = error_line();
        declare @errorMessage nvarchar(2048) = error_message();

        declare @warningMessage nvarchar(max) = case
                                                  when @errorProcedure is null then N'Warning MSRep000g: Error ' + convert(nvarchar(10), @errorNumber) + N' at line ' + convert(nvarchar(10), @errorLine) + N'.'
                                                  else N'Warning MSRep000g: Error ' + convert(nvarchar(10), @errorNumber) + N' in procedure or trigger named [' + @errorProcedure + N'] failed at line ' + convert(nvarchar(10), @errorLine) + N'.'
                                                end;

        if exists (select *                            -- Caller has Alter Trace permissions.
                   from sys.server_permissions as SP
                   where SP.class = 100 and
                         SP.type in ('ALTR', 'CL') and
                         SP.State in ('G', 'W'))
        begin
          raiserror(@warningMessage, @errorSeverity, 0) with log, nowait;
        end
        else
        begin
          raiserror(@warningMessage, @errorSeverity, 0) with nowait;
        end;

        if exists (select *                            -- Caller has Alter Trace permissions.
                   from sys.server_permissions as SP
                   where SP.class = 100 and
                         SP.type in ('ALTR', 'CL') and
                         SP.State in ('G', 'W'))
        begin
          raiserror(@errorMessage, @errorSeverity, @errorState) with log, nowait;
        end
        else
        begin
          raiserror(@errorMessage, @errorSeverity, @errorState) with nowait;
        end;
      end catch;
    end
