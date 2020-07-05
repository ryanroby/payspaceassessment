create trigger [Trigger_Repository_Microsoft.VsCodeIndex_Drop]
    on all server
    for drop_database
    as
    begin
      if eventdata().value('(/EVENT_INSTANCE/DatabaseName)[1]','nvarchar(max)') = N'Microsoft.VsCodeIndex'
      begin
        if exists (select * from sys.server_triggers where name = N'Trigger_Repository_Microsoft.VsCodeIndex_Repository.Item_Logon_SetSecurityClaims')
        begin
          drop trigger [Trigger_Repository_Microsoft.VsCodeIndex_Repository.Item_Logon_SetSecurityClaims] on all server;
        end;

        if exists (select * from sys.server_triggers where name = N'Trigger_Repository_Microsoft.VsCodeIndex_Drop')
        begin
          -- Not renamed so can delete.
          drop trigger [Trigger_Repository_Microsoft.VsCodeIndex_Drop] on all server;
        end;
      end;
    end
